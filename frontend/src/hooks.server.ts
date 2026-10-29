// src/hooks.server.ts
import { apiFetch, type apiResponse } from '#lib/api/api.ts';
import type { UserDetailed } from '#lib/api/userTypes.ts';
import type { Handle } from '@sveltejs/kit/hooks';

export const handle: Handle = async ({ event, resolve }) => {
	const sessionToken = event.cookies.get('jwt');
	const refreshToken = event.cookies.get('refresh');

	if (!sessionToken) {
		event.locals.user = null;
		
	// if (!refreshToken){
		return resolve(event);
	// }
	}

	const userResponse: apiResponse<UserDetailed> = await apiFetch<Object, UserDetailed>("http://localhost:5062/api/track", null, "GET", {'Authorization': `Bearer ${sessionToken}`});
	
	if (userResponse.status !== 200){
		event.locals.user = null;
		return resolve(event);
	}

	const user = userResponse.data;

	if (user) {
		event.locals.user = {
			username: user.username,
			artists: user.artists,
			tracks: user.tracks,
			audioFiles: user.audioFiles
		};
	} else {
		event.locals.user = null;
	}

	return resolve(event);
};
