export type apiFetchType = 'GET' | 'POST' | 'PUT' | 'DELETE'

export interface apiResponse<T>{
    data: T | null,
    status: number
}

export async function apiFetch<TRequest, TResponse>(url: string, request: TRequest | null, method: apiFetchType = "GET", headers:Record<string, string> | null = null): Promise<apiResponse<TResponse>> {
    const response = await fetch(url, {
        method: method,
        headers: {
            "Content-Type": "application/json",
            ...(headers ?? {})
        },
        ...(request != null ? { body: JSON.stringify(request)}: {})
    });

    if(response.ok){
        return {
            data: await response.json() as TResponse,
            status: response.status
        }     
    }

    if(response.status !== 500){
        return {
            data: null,
            status: response.status
        };
    }

    throw new Error(`Error: ${response.status}`);

}