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