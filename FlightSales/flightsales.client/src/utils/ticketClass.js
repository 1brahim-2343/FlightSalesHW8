export const TICKET_CLASSES = [
    { value: 0, label: 'Economy' },
    { value: 1, label: 'Premium Economy' },
    { value: 2, label: 'Business' },
    { value: 3, label: 'First' },
];

export const ticketClassLabel = (value) =>
    TICKET_CLASSES.find((c) => c.value === value)?.label ?? 'Unknown';
