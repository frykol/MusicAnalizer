<script lang="ts">
	import type { TrackDetailed } from "#lib/api/tracks.ts";
    import type {PageProps} from "./$types"
    import Button from "#lib/ui/Button.svelte";

    let {data}: PageProps = $props();
    let track: TrackDetailed = $derived(data.track);
    let readableDate: string = $derived.by(() => {
        if(track.audioFile !== null){
            return track.audioFile.uploadedDate.toLocaleDateString('pl-PL');
        }
        else{
            return "Unknown";
        }
    });
    
</script>
<div class="flex flex-col h-dvh">
    <div id="top" class="flex flex-row border-b-2 border-b-text-damped pb-2 items-center">
        <Button name="Back" href="/tracks" />
        <div class="flex flex-1 justify-center">
            <h1 class="bg-elevated px-4 py-2 rounded-md text-3xl leading-none">{track.title}</h1>
        </div>
    </div>
    <div class="flex flex-row flex-1">
        <div id="left" class="border-r-2 border-r-text-damped pt-2 pr-2 w-72">
            <img src="/images/music_placeholder.webp" alt="music-cover">
            <p class="mt-2 text-text-damped text-xl">Author: {track.artist.name}</p>
            <p class="mt-2 text-text-damped text-xl">Uploaded at: {readableDate}</p>
            <p class="mt-2 text-text-damped text-xl">Genre: {track.genre}</p>
        </div>
        <div id="main" class="min-w-0 flex-1 p-2">
            asd
        </div>
        <div id="right" class="bg-red-500">
            a
        </div>
    </div>
</div>