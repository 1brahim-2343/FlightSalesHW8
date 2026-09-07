import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { flightsApi } from '../api/flights';
import { buildLocationString } from '../utils/location';
import { toApiDateTime } from '../utils/format';
import StatusMessage from '../components/StatusMessage';

const emptyForm = {
    originCode: '', originCity: '', originCountry: '',
    destinationCode: '', destinationCity: '', destinationCountry: '',
    departureTime: '', arrivalTime: '',
};

const AddFlightPage = () => {
    const navigate = useNavigate();
    const [form, setForm] = useState(emptyForm);
    const [error, setError] = useState(null);
    const [submitting, setSubmitting] = useState(false);

    const handleChange = (field) => (event) =>
        setForm((prev) => ({ ...prev, [field]: event.target.value }));

    const handleSubmit = async (event) => {
        event.preventDefault();
        setError(null);
        setSubmitting(true);
        try {
            const dto = {
                origin: buildLocationString(form.originCode, form.originCity, form.originCountry),
                destination: buildLocationString(form.destinationCode, form.destinationCity, form.destinationCountry),
                departureTime: toApiDateTime(form.departureTime),
                arrivalTime: toApiDateTime(form.arrivalTime),
            };
            const created = await flightsApi.add(dto);
            navigate(`/flights/${created.id}`);
        } catch (err) {
            setError(err.message);
        } finally {
            setSubmitting(false);
        }
    };

    return (
        <section className="page-narrow">
            <h1>Add a flight</h1>
            <form className="form" onSubmit={handleSubmit}>
                <fieldset>
                    <legend>Origin</legend>
                    <label>
                        Airport code
                        <input required maxLength={3} placeholder="GYD" value={form.originCode} onChange={handleChange('originCode')} />
                    </label>
                    <label>
                        City
                        <input required placeholder="Baku" value={form.originCity} onChange={handleChange('originCity')} />
                    </label>
                    <label>
                        Country
                        <input required placeholder="Azerbaijan" value={form.originCountry} onChange={handleChange('originCountry')} />
                    </label>
                </fieldset>

                <fieldset>
                    <legend>Destination</legend>
                    <label>
                        Airport code
                        <input required maxLength={3} placeholder="JFK" value={form.destinationCode} onChange={handleChange('destinationCode')} />
                    </label>
                    <label>
                        City
                        <input required placeholder="New York" value={form.destinationCity} onChange={handleChange('destinationCity')} />
                    </label>
                    <label>
                        Country
                        <input required placeholder="United States" value={form.destinationCountry} onChange={handleChange('destinationCountry')} />
                    </label>
                </fieldset>

                <label>
                    Departure time
                    <input required type="datetime-local" value={form.departureTime} onChange={handleChange('departureTime')} />
                </label>
                <label>
                    Arrival time
                    <input required type="datetime-local" value={form.arrivalTime} onChange={handleChange('arrivalTime')} />
                </label>

                {error && <StatusMessage tone="error">{error}</StatusMessage>}
                <button type="submit" disabled={submitting}>{submitting ? 'Adding…' : 'Add flight'}</button>
            </form>
        </section>
    );
};

export default AddFlightPage;
