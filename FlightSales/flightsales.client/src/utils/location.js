// Backend dest/origin foormat "GYD | BAKU, AZERBAIJAN"
export const buildLocationString = (code, city, country) =>
    `${code.trim().toUpperCase()} | ${city.trim().toUpperCase()}, ${country.trim().toUpperCase()}`;

export const parseLocationString = (value) => {
    if (!value) return { code: '', city: '', country: '' };
    const [code, rest] = value.split('|').map((part) => part.trim());
    const [city, country] = (rest || '').split(',').map((part) => part.trim());
    return { code: code || '', city: city || '', country: country || '' };
};
