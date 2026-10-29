import { error } from "@sveltejs/kit";
import type { PageProps, PageServerLoad } from "../$types";
import type { TrackDetailed } from "#lib/api/tracksTypes.ts";
import { apiFetch, type apiResponse } from "#lib/api/api.ts";


export const load: PageServerLoad = async({params}) =>{
    const response: apiResponse<TrackDetailed> = await apiFetch<Object,TrackDetailed>(`http://localhost:5062/api/track/${params.id}`, null, 'GET');

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