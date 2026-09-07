import { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import { flightsApi } from '../api/flights';
import { formatDateTime, formatDuration } from '../utils/format';
import { parseLocationString } from '../utils/location';
import StatusMessage from '../components/StatusMessage';

const FlightsListPage = () => {
    const [flights, setFlights] = useState(null);
    const [error, setError] = useState(null);

    const loadFlights = async () => {
        setError(null);
        try {
            const data = await flightsApi.getAll();
            setFlights(data);
        } catch (err) {
            setError(err.message);
        }
    };

    useEffect(() => {
        loadFlights();
    }, []);

    if (error) return <StatusMessage tone="error" title="Couldn't load flights">{error}</StatusMessage>;
    if (!flights) return <StatusMessage title="Loading flights…" />;
    if (flights.length === 0) {
        return (
            <StatusMessage title="No flights yet">
                <Link to="/flights/new">Add the first flight</Link> to get the board going.
            </StatusMessage>
        );
    }

    return (
        <section className="board">
            <div className="board-header">
                <h1>Departures</h1>
                <span className="board-count">{flights.length} flights</span>
            </div>
            <div className="board-table">
                <div className="board-row board-row--head">
                    <span>Code</span>
                    <span>From</span>
                    <span>To</span>
                    <span>Departs</span>
                    <span>Duration</span>
                    <span></span>
                </div>
                {flights.map((flight) => {
                    const origin = parseLocationString(flight.origin);
                    const destination = parseLocationString(flight.destination);
                    return (
                        <Link to={`/flights/${flight.id}`} key={flight.id} className="board-row">
                            <span className="mono">{origin.code}→{destination.code}</span>
                            <span>{origin.city}</span>
                            <span>{destination.city}, {flight.destinationCountry}</span>
                            <span className="mono">{formatDateTime(flight.departureTime)}</span>
                            <span className="mono">{formatDuration(flight.departureTime, flight.arrivalTime)}</span>
                            <span className="board-row-arrow">→</span>
                        </Link>
                    );
                })}
            </div>
        </section>
    );
};

export default FlightsListPage;
