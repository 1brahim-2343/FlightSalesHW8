import { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { flightsApi } from '../api/flights';
import { toDateTimeLocalValue, toApiDateTime } from '../utils/format';
import { parseLocationString } from '../utils/location';
import StatusMessage from '../components/StatusMessage';

const EditFlightPage = () => {
    const { id } = useParams();
    const navigate = useNavigate();

    const [flight, setFlight] = useState(null);
    const [form, setForm] = useState({ departureTime: '', arrivalTime: '' });
    const [loadError, setLoadError] = useState(null);
    const [saveError, setSaveError] = useState(null);
    const [submitting, setSubmitting] = useState(false);

    useEffect(() => {
        (async () => {
            try {
                const data = await flightsApi.getById(id);
                setFlight(data);
                setForm({
                    departureTime: toDateTimeLocalValue(data.departureTime),
                    arrivalTime: toDateTimeLocalValue(data.arrivalTime),
                });
            } catch (err) {
                setLoadError(err.message);
            }
        })();
    }, [id]);

    const handleChange = (field) => (event) =>
        setForm((prev) => ({ ...prev, [field]: event.target.value }));

    const handleSubmit = async (event) => {
        event.preventDefault();
        setSaveError(null);
        setSubmitting(true);
        try {
            await flightsApi.update(id, {
                departureTime: toApiDateTime(form.departureTime),
                arrivalTime: toApiDateTime(form.arrivalTime),
            });
            navigate(`/flights/${id}`);
        } catch (err) {
            setSaveError(err.message);
        } finally {
            setSubmitting(false);
        }
    };

    if (loadError) return <StatusMessage tone="error" title="Couldn't load this flight">{loadError}</StatusMessage>;
    if (!flight) return <StatusMessage title="Loading flight…" />;

    const origin = parseLocationString(flight.origin);
    const destination = parseLocationString(flight.destination);

    return (
        <section className="page-narrow">
            <h1>Edit flight</h1>
            <p className="mono muted">{origin.code} → {destination.code}</p>
            <p className="muted">Route can't be changed here — only the schedule.</p>

            <form className="form" onSubmit={handleSubmit}>
                <label>
                    Departure time
                    <input required type="datetime-local" value={form.departureTime} onChange={handleChange('departureTime')} />
                </label>
                <label>
                    Arrival time
                    <input required type="datetime-local" value={form.arrivalTime} onChange={handleChange('arrivalTime')} />
                </label>
                {saveError && <StatusMessage tone="error">{saveError}</StatusMessage>}
                <button type="submit" disabled={submitting}>{submitting ? 'Saving…' : 'Save changes'}</button>
            </form>
        </section>
    );
};

export default EditFlightPage;
