import { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import { ticketsApi } from '../api/tickets';
import { ticketStorage } from '../utils/ticketStorage';
import { formatDateTime, formatMoney } from '../utils/format';
import { parseLocationString } from '../utils/location';
import { ticketClassLabel } from '../utils/ticketClass';
import StatusMessage from '../components/StatusMessage';

const MyTicketsPage = () => {
    const [tickets, setTickets] = useState(null);
    const [error, setError] = useState(null);
    const [removingId, setRemovingId] = useState(null);

    const loadTickets = async () => {
        setError(null);
        const ids = ticketStorage.getIds();
        const results = await Promise.all(
            ids.map(async (id) => {
                try {
                    return await ticketsApi.getById(id);
                } catch {
                    ticketStorage.removeId(id); 
                    return null;
                }
            })
        );
        setTickets(results.filter(Boolean));
    };

    useEffect(() => {
        loadTickets();
    }, []);

    const handleDelete = async (id) => {
        if (!window.confirm('Cancel this ticket?')) return;
        setRemovingId(id);
        setError(null);
        try {
            await ticketsApi.remove(id);
            ticketStorage.removeId(id);
            setTickets((prev) => prev.filter((t) => t.id !== id));
        } catch (err) {
            setError(err.message);
        } finally {
            setRemovingId(null);
        }
    };

    if (!tickets) return <StatusMessage title="Loading your tickets…" />;
    if (tickets.length === 0) {
        return (
            <StatusMessage title="No tickets yet">
                Tickets you book are remembered on this device. <Link to="/">Browse flights</Link> to book one.
            </StatusMessage>
        );
    }

    return (
        <section className="tickets">
            <h1>My tickets</h1>
            {error && <StatusMessage tone="error">{error}</StatusMessage>}
            <div className="ticket-list">
                {tickets.map((ticket) => {
                    const origin = parseLocationString(ticket.origin);
                    const destination = parseLocationString(ticket.destination);
                    return (
                        <div className="ticket-card" key={ticket.id}>
                            <div className="ticket-card-route">
                                <span className="mono">{origin.code} → {destination.code}</span>
                                <span>{ticketClassLabel(ticket.class)}</span>
                            </div>
                            <div className="ticket-card-details">
                                <span>{ticket.passengerFullname}</span>
                                <span>Seat {ticket.seatNumber}</span>
                                <span className="mono">{formatDateTime(ticket.departureTime)}</span>
                                <span>{formatMoney(ticket.price)}</span>
                            </div>
                            <div className="ticket-card-actions">
                                <Link to={`/flights/${ticket.flight.id}`}>View flight</Link>
                                <button
                                    type="button"
                                    className="link-danger"
                                    disabled={removingId === ticket.id}
                                    onClick={() => handleDelete(ticket.id)}
                                >
                                    {removingId === ticket.id ? 'Cancelling…' : 'Cancel'}
                                </button>
                            </div>
                        </div>
                    );
                })}
            </div>
        </section>
    );
};

export default MyTicketsPage;
