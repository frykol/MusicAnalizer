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
    duration: number,
    codec: string,
    bitrate: number | null,
    sampleRate: number,
    channels: number,
    size: number
}

export interface TrackDetailed extends TrackSimple{
    artist: TrackArtist,
    audioFile: TrackAudioFile
}

export interface TrackCreate{
    title: string,
    genre: string,
    artistId: number
}

export interface apiResponse<T>{
    data: T | null,
    status: number
}

export async function createTrack<T>(url: string, track: TrackCreate): Promise<apiResponse<T>> {
    const response = await fetch(url, {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(track)
    });



    if(response.ok){
        return {
            data: await response.json() as T,
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


export async function getTracks<T>(url: string): Promise<apiResponse<T>> {
    const response = await fetch(url); 

    if (response.status === 404){
        return {
            data: null,
            status: response.status
        };
    }

    if(!response.ok){
        throw new Error(`Error: ${response.status}`);
    }

    return {
        data: await response.json() as T,
        status: response.status
    }
}   