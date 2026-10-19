import { getTracks, type TrackArtist, type apiResponse } from "#lib/api/tracks.ts";
import { error } from "@sveltejs/kit";
import type { PageServerLoad } from "./$types";


export const load: PageServerLoad = async({params}) => {
    const response: apiResponse<TrackArtist[]> = await getTracks<TrackArtist[]>(`http://localhost:8080/api/artists`);
    if (!response.data) {
        error(404, "Artists not found");
    }
    return { artists: response.data || [] };
}