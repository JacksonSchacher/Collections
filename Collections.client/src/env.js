export const dev = window.location.origin.includes('localhost')
export const baseURL = dev ? 'https://localhost:5001' : ''
export const useSockets = false
export const domain = 'plant-collector.us.auth0.com'
export const clientId = 'A1PdvqVPMkuNKplKufHfGVDKx88GQ2Lw'
export const audience = 'https://collections.com'
