import { getTracks, type apiResponse, type TrackDetailed } from "#lib/api/tracks.ts";
import { error } from "@sveltejs/kit";
import type { PageProps, PageServerLoad } from "../$types";


export const load: PageServerLoad = async({params}) =>{
    console.log(params.id);
    const response: apiResponse<TrackDetailed> = await getTracks(`http://localhost:5062/api/track/${params.id}`);

    if (response.data === null){
        error(404, 'Track not found');
    }

    if (response.data.audioFile !== null){
        response.data.audioFile.uploadedDate = new Date(response.data.audioFile.uploadedDate);
    }
    console.log(response.data);

    return {track: response.data};
    
};