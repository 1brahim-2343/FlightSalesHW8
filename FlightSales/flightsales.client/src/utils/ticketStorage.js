const STORAGE_KEY = 'flightSales.myTicketIds';

const readIds = () => {
    try {
        const raw = localStorage.getItem(STORAGE_KEY);
        return raw ? JSON.parse(raw) : [];
    } catch {
        return [];
    }
};

const writeIds = (ids) => localStorage.setItem(STORAGE_KEY, JSON.stringify(ids));

export const ticketStorage = {
    getIds: () => readIds(),
    addId: (id) => {
        const ids = readIds();
        if (!ids.includes(id)) writeIds([...ids, id]);
    },
    removeId: (id) => writeIds(readIds().filter((existing) => existing !== id)),
};
