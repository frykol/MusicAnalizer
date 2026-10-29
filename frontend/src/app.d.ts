// See https://svelte.dev/docs/kit/types#app.d.ts

import type { UserDetailed } from "#lib/api/userTypes.ts";

// for information about these interfaces
declare global {
	namespace App {
		// interface Error {}
		interface Locals {
			user: UserDetailed | null;
		}
		// interface PageData {}
		// interface PageState {}
		// interface Platform {}
	}
}

export {};
