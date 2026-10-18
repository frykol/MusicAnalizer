import { getTracks, type TrackDetailed } from "#lib/api/tracks.ts";
import { error } from "@sveltejs/kit";
import type { PageProps, PageServerLoad } from "../$types";


export const load: PageServerLoad = async({params}) =>{
    console.log(params.id);
    const track: TrackDetailed | null = await getTracks(`http://localhost:5062/api/track/${params.id}`);

    if (track === null){
        error(404, 'Track not found');
    }

    if (track.audioFile !== null){
        track.audioFile.uploadedDate = new Date(track.audioFile.uploadedDate);
    }
    console.log(track);

    return {track};
    
};