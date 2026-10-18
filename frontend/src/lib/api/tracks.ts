export interface TrackSimple{
    id: number,
    title: string,
    genre: string
}

export interface TrackArtist{
    id: number,
    name: string
}

export interface TrackAudioFile{
    originalFileName: string,
    uploadedDate: Date,
    duration: number
}

export interface TrackDetailed extends TrackSimple{
    artist: TrackArtist,
    audioFile: TrackAudioFile
}


export async function getTracks<T>(url: string): Promise<T | null> {
    const response = await fetch(url); 

    if (response.status === 404){
        return null;
    }

    if(!response.ok){
        throw new Error(`Error: ${response.status}`);
    }

    return response.json() as Promise<T>
}   