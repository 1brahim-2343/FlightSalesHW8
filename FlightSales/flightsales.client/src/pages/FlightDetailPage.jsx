import { useEffect, useState } from 'react';
import { useNavigate, useParams, Link } from 'react-router-dom';
import { flightsApi } from '../api/flights';
import { ticketsApi } from '../api/tickets';
import { ticketStorage } from '../utils/ticketStorage';
import { formatDateTime, formatDuration } from '../utils/format';
import { parseLocationString } from '../utils/location';
import { TICKET_CLASSES } from '../utils/ticketClass';
import StatusMessage from '../components/StatusMessage';

const emptyBooking = { seatNumber: '', passengerFullname: '', price: '', ticketClass: 0 };

const FlightDetailPage = () => {
    const { id } = useParams();
    const navigate = useNavigate();

    const [flight, setFlight] = useState(null);
    const [loadError, setLoadError] = useState(null);

    const [booking, setBooking] = useState(emptyBooking);
    const [bookingError, setBookingError] = useState(null);
    const [confirmedTicket, setConfirmedTicket] = useState(null);
    const [submitting, setSubmitting] = useState(false);

    const [deleteError, setDeleteError] = useState(null);

    const loadFlight = async () => {
        setLoadError(null);
        try {
            const data = await flightsApi.getById(id);
            setFlight(data);
        } catch (err) {
            setLoadError(err.message);
        }
    };

    useEffect(() => {
        loadFlight();
    }, [id]);

    const handleBookingChange = (field) => (event) =>
        setBooking((prev) => ({ ...prev, [field]: event.target.value }));

    const handleBookingSubmit = async (event) => {
        event.preventDefault();
        setBookingError(null);
        setSubmitting(true);
        try {
            const dto = {
                flightId: id,
                class: Number(booking.ticketClass),
                seatNumber: booking.seatNumber.trim(),
                passengerFullname: booking.passengerFullname.trim(),
                price: Number(booking.price),
            };
            const newTicket = await ticketsApi.add(dto);
            ticketStorage.addId(newTicket.id);
            setConfirmedTicket(newTicket);
            setBooking(emptyBooking);
        } catch (err) {
            setBookingError(err.message);
        } finally {
            setSubmitting(false);
        }
    };

    const handleDeleteFlight = async () => {
        if (!window.confirm('Delete this flight? This cannot be undone.')) return;
        setDeleteError(null);
        try {
            await flightsApi.remove(id);
            navigate('/');
        } catch (err) {
            setDeleteError(err.message);
        }
    };

    if (loadError) return <StatusMessage tone="error" title="Couldn't load this flight">{loadError}</StatusMessage>;
    if (!flight) return <StatusMessage title="Loading flight…" />;

    const origin = parseLocationString(flight.origin);
    const destination = parseLocationString(flight.destination);

    return (
        <section className="detail">
            <div className="detail-route">
                <div className="detail-point">
                    <span className="mono detail-code">{origin.code}</span>
                    <span>{origin.city}</span>
                </div>
                <div className="detail-path">
                    <span className="mono">{formatDuration(flight.departureTime, flight.arrivalTime)}</span>
                    <div className="detail-line" />
                </div>
                <div className="detail-point">
                    <span className="mono detail-code">{destination.code}</span>
                    <span>{destination.city}, {flight.destinationCountry}</span>
                </div>
            </div>

            <dl className="detail-facts">
                <div><dt>Departs</dt><dd className="mono">{formatDateTime(flight.departureTime)}</dd></div>
                <div><dt>Arrives</dt><dd className="mono">{formatDateTime(flight.arrivalTime)}</dd></div>
                <div><dt>Origin country</dt><dd>{flight.originCountry}</dd></div>
                <div><dt>Destination country</dt><dd>{flight.destinationCountry}</dd></div>
            </dl>

            <div className="detail-actions">
                <Link to={`/flights/${id}/edit`}>Edit flight</Link>
                <button type="button" className="link-danger" onClick={handleDeleteFlight}>Delete flight</button>
            </div>
            {deleteError && <StatusMessage tone="error">{deleteError}</StatusMessage>}

            <div className="booking-panel">
                <h2>Book a ticket</h2>

                {confirmedTicket ? (
                    <StatusMessage tone="success" title="Booked">
                        Seat {confirmedTicket.seatNumber} for {confirmedTicket.passengerFullname} — ticket id{' '}
                        <span className="mono">{confirmedTicket.id}</span>. Saved to{' '}
                        <Link to="/my-tickets">My tickets</Link>.
                    </StatusMessage>
                ) : (
                    <form className="form" onSubmit={handleBookingSubmit}>
                        <label>
                            Passenger full name
                            <input
                                required
                                value={booking.passengerFullname}
                                onChange={handleBookingChange('passengerFullname')}
                            />
                        </label>
                        <label>
                            Seat number
                            <input
                                required
                                placeholder="12A"
                                value={booking.seatNumber}
                                onChange={handleBookingChange('seatNumber')}
                            />
                        </label>
                        <label>
                            Class
                            <select value={booking.ticketClass} onChange={handleBookingChange('ticketClass')}>
                                {TICKET_CLASSES.map((c) => (
                                    <option key={c.value} value={c.value}>{c.label}</option>
                                ))}
                            </select>
                        </label>
                        <label>
                            Price
                            <input
                                required
                                type="number"
                                min="0"
                                step="0.01"
                                value={booking.price}
                                onChange={handleBookingChange('price')}
                            />
                        </label>
                        {bookingError && <StatusMessage tone="error">{bookingError}</StatusMessage>}
                        <button type="submit" disabled={submitting}>
                            {submitting ? 'Booking…' : 'Book ticket'}
                        </button>
                    </form>
                )}
            </div>
        </section>
    );
};

export default FlightDetailPage;
