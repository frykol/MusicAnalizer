<script lang="ts">
	import type { UserDetailed } from "#lib/api/userTypes.ts";

    interface Props{
        user: UserDetailed | null
    };

    let { user }:Props = $props();
</script>

<div class="bg-elevated p-4 shadow-lg flex flex-row justify-between items-center">
    <h1 class="text-2xl"><a href="/"><b>Music</b> analizer</a></h1>
    <div class="flex flex-col items-center justify-center">
        {#if user}
            <p>{user.username}</p>
        {:else}
            <p class="font-bold">Not logged in</p>
            <p>(<a href="/auth" class="cursor-pointer text-blue-500 underline underline-offset-2">Log in now</a>)</p>
        {/if}
    </div>
</div>