import { type TrackSimple } from "#lib/api/tracksTypes.ts";
import { type apiResponse, apiFetch } from "#lib/api/api.ts";
import { error } from "@sveltejs/kit";
import type { PageServerLoad } from "./$types";

export const load: PageServerLoad = async() =>{
  //let response: apiResponse<TrackSimple[]> = await getTracks<TrackSimple[]>("http://localhost:5062/api/track");  
  let response :apiResponse<TrackSimple[]> = await apiFetch<Object, TrackSimple[]>("http://localhost:5062/api/track", null, "GET");

  if (!response.data) {
    error(404, "Tracks not found");
  }

  return {tracks: response.data || []};
};