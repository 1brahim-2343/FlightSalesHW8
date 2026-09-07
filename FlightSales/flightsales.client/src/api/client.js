const jsonHeaders = { 'Content-Type': 'application/json' };

async function parseErrorMessage(response) {
    try {
        const data = await response.json();
        if (typeof data === 'string') return data;
        if (data?.message) return data.message;
        if (data?.errors) return Object.values(data.errors).flat().join(' ');
        return JSON.stringify(data);
    } catch {
        return response.statusText || 'Something went wrong';
    }
}

async function request(url, options = {}) {
    const response = await fetch(url, options);

    if (response.status === 204) return null;

    if (!response.ok) {
        const message = await parseErrorMessage(response);
        throw new Error(message);
    }

    const text = await response.text();
    return text ? JSON.parse(text) : null;
}

export const apiClient = {
    get: (url) => request(url),
    post: (url, body) => request(url, { method: 'POST', headers: jsonHeaders, body: JSON.stringify(body) }),
    put: (url, body) => request(url, { method: 'PUT', headers: jsonHeaders, body: JSON.stringify(body) }),
    delete: (url) => request(url, { method: 'DELETE' }),
};
