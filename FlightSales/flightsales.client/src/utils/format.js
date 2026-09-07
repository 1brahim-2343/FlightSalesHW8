export const formatDateTime = (isoString) => {
    const date = new Date(isoString);
    return date.toLocaleString(undefined, {
        month: 'short',
        day: 'numeric',
        hour: '2-digit',
        minute: '2-digit',
    });
};

export const formatTime = (isoString) => {
    const date = new Date(isoString);
    return date.toLocaleTimeString(undefined, { hour: '2-digit', minute: '2-digit' });
};

export const formatDuration = (departureIso, arrivalIso) => {
    const minutes = Math.round((new Date(arrivalIso) - new Date(departureIso)) / 60000);
    if (minutes < 0) return '—';
    const hours = Math.floor(minutes / 60);
    const mins = minutes % 60;
    return `${hours}h ${mins.toString().padStart(2, '0')}m`;
};

export const formatMoney = (amount) =>
    new Intl.NumberFormat(undefined, { style: 'currency', currency: 'USD' }).format(amount);

export const toApiDateTime = (localValue) =>
    localValue.length === 16 ? `${localValue}:00` : localValue;

export const toDateTimeLocalValue = (isoString) => {
    if (!isoString) return '';
    const date = new Date(isoString);
    const pad = (n) => n.toString().padStart(2, '0');
    return `${date.getFullYear()}-${pad(date.getMonth() + 1)}-${pad(date.getDate())}T${pad(date.getHours())}:${pad(date.getMinutes())}`;
};
