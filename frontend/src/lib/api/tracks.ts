export interface TrackSimple{
    id: number,
    title: string,
    genre: string,
}

export async function getTracks<T>(url: string): Promise<T> {
    const response = await fetch(url);

    if(!response.ok){
        throw new Error(`Error: ${response.status}`);
    }

    const data: T = await response.json();

    return data;
}   