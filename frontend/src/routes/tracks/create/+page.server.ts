import { type apiResponse, apiFetch } from "#lib/api/api.ts";
import { type TrackArtist } from "#lib/api/tracksTypes.ts";
import { error } from "@sveltejs/kit";
import type { PageServerLoad } from "./$types";


export const load: PageServerLoad = async({params}) => {
    //const response: apiResponse<TrackArtist[]> = await getTracks<TrackArtist[]>(`http://localhost:5062/api/artist`);
    let response :apiResponse<TrackArtist[]> = await apiFetch<Object, TrackArtist[]>("http://localhost:5062/api/artist", null, "GET");
    console.log(response.data);
    if (!response.data) {
        error(404, "Artists not found");
    }


    return { artists: response.data || [] };
}