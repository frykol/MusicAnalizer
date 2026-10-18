import { getTracks, type TrackSimple } from "#lib/api/tracks.ts";
import type { PageServerLoad } from "./$types";

export const load: PageServerLoad = async() =>{
  let tracks: TrackSimple[] = await getTracks<TrackSimple[]>("http://localhost:5062/api/track");  
  
  return {tracks};
};