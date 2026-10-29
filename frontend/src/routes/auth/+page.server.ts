import { register } from "module";
import type { Actions } from "./$types";

export const actions = {
    login: async({request}) => {
        const data = await request.formData();
        const username = data.get("username");
        const password = data.get("password");

        console.log(username, password);
    },
    register: async(event) => {

    }
} satisfies Actions;