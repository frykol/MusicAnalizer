import { getTracks, type apiResponse, type TrackSimple } from "#lib/api/tracks.ts";
import { error } from "@sveltejs/kit";
import type { PageServerLoad } from "./$types";

export const load: PageServerLoad = async() =>{
  let response: apiResponse<TrackSimple[]> = await getTracks<TrackSimple[]>("http://localhost:5062/api/track");  

  if (!response.data) {
    error(404, "Tracks not found");
  }

  return {tracks: response.data || []};
};