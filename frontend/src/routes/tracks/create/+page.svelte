<script lang="ts">
	import type { TrackCreate, TrackSimple } from "#lib/api/tracksTypes.ts";
    import { type apiResponse, apiFetch } from "#lib/api/api.ts";
    import Button from "#lib/ui/Button.svelte";
    import type { PageProps } from "./$types";

    let {data}: PageProps = $props();

    let statusCreate: boolean | null = $state(null);

    let formData: TrackCreate = $derived.by(() => {
        return {
            title: "",
            artistId: data.artists[0].id,
            genre: "",
            audioFile: null
        }
    });

    async function handleSubmit(event: Event){
        event.preventDefault();
        console.log(formData);
        statusCreate = null;
        try{
            let createResponse: apiResponse<TrackSimple> = await apiFetch<TrackCreate,TrackSimple>("http://localhost:5062/api/track", formData, 'POST');
            console.log(createResponse.status);
            if(createResponse.status === 201){
                statusCreate = true;
            }
            else{
                statusCreate = false;
            }
        }
        catch (e){
            console.error("Error while creating track: ", e)
            statusCreate = false;
        }
        
    }
</script>

<div>
    <div class="flex row border-b-2 border-b-text-damped pb-2">
        <Button name="Back" href="/tracks" />
        <h1 class="text-3xl font-bold text-center  flex-1">Add your track to analysis</h1>
    </div>
    <form class="flex flex-col p-2" onsubmit={handleSubmit}>
        <label for="title" class="text-xl">Title</label>
        <input bind:value={formData.title} type="text" name="title" id="title" class="border-2 border-elevated rounded-md px-2 py-1 text-xl">
        <label for="artist" class="text-xl">Artist</label>
        <select bind:value={formData.artistId} name="genre" id="genre" class="border-2 border-elevated rounded-md px-2 py-1 text-xl">
            {#each data.artists as artist}
                <option value={artist.id}>{artist.name}</option>
            {/each}
        </select>
        <label for="genre" class="text-xl">Genre</label>
        <input bind:value={formData.genre} type="text" name="genre" id="genre" class="border-2 border-elevated rounded-md px-2 py-1 text-xl">
        <label for="audioFile" class="text-xl">Audio file</label>
        <input type="file" name="audioFile" id="audioFile" class="border-2 border-elevated rounded-md px-2 py-1 text-xl">
        <button type="submit" class="text-xl border-2 border-elevated px-2 py-1 rounded-md cursor-pointer hover:bg-elevated transition-colors mt-2">Submit</button>
    </form>

    {#if statusCreate === true}
        <p class="text-green-500 text-xl">Track created successfully</p>
    {:else if statusCreate === false}
        <p class="text-red-500 text-xl">Error creating track</p>
    {/if}
</div>