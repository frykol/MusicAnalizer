import { getTracks, type apiResponse, type TrackDetailed, type TrackAudioFile } from "#lib/api/tracks.ts";
import { error } from "@sveltejs/kit";
import type { PageProps, PageServerLoad } from "../$types";


export const load: PageServerLoad = async({params}) =>{
    const response: apiResponse<TrackDetailed> = await getTracks(`http://localhost:5062/api/track/${params.id}`);

    if (response.data === null){
        error(404, 'Track not found');
    }


    if (response.data.audioFile !== null){
        response.data.audioFile.uploadedDate = new Date(response.data.audioFile.uploadedDate);
    }
    
    // let defaultAudioFile: TrackAudioFile = {
    //     originalFileName: "Unknown",
    //     bitrate: 0,
    //     channels: 0,

    // };


    return {track: response.data};
    
};