import { getTokenWorkaround } from "@/app/actions/authActions";

const baseURL = process.env.API_URL;

async function get(url: string) {
    const requestOptions : RequestInit= {
        method: 'GET',
        headers: await getHeaders(),
        mode: 'cors'
    }

    const response = await fetch(baseURL + url, requestOptions);

    return await handleResponse(response);

}

async function post(url: string, body: {}) {
    const requestOptions : RequestInit = {
        method: 'POST',
        headers: await getHeaders(),
        body: JSON.stringify(body),
        mode: 'cors'
    }

    const response = await fetch(baseURL + url, requestOptions);
    return await handleResponse(response);
}

async function put(url: string, body: {}) {
    const requestOptions : RequestInit = {
        method: 'PUT',
        headers: await getHeaders(),
        body: JSON.stringify(body),
        mode: 'cors'
    }

    const response = await fetch(baseURL + url, requestOptions);
    return await handleResponse(response);
}

async function del(url: string) {
    const requestOptions : RequestInit= {
        method: 'DELETE',
        headers: await getHeaders(),
        mode:'cors'
    }

    const response = await fetch(baseURL + url, requestOptions);
    return await handleResponse(response);
}

async function getHeaders() {
    const token = await getTokenWorkaround();
    const headers = { 'Content-type': 'application/json' } as any;
    if (token) {
        headers.Authorization = 'Bearer ' + token.access_token
    }
    return headers;
}

async function handleResponse(response: Response) {
    const text = await response.text();
    
    //const data = text && JSON.parse(text);
    let data;
    try {
        data = JSON.parse(text)
    } catch (error) {
        data = text;
    }
    

    if (response.ok) {
        return data || response.statusText;
    } else {
        const error = {
            status: response.status,
            message: typeof data ==='string' && data.length > 0 ?  data : response.statusText
        }

        return {error};
    }

}

export const fetchWrapper = { get, post, put, del }