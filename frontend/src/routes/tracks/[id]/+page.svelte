<script lang="ts">
	import type { TrackAudioFile, TrackDetailed } from "#lib/api/tracks.ts";
    import type {PageProps} from "./$types"
    import Button from "#lib/ui/Button.svelte";
	import ListItem from "./ListItem.svelte";

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

    let audioFileMetadata: TrackAudioFile | null = $derived(track.audioFile);


</script>
<div class="flex flex-col h-dvh">
    <div id="top" class="flex flex-row border-b-2 border-b-text-damped pb-2 items-center">
        <Button name="Back" href="/tracks" color="default" />
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
        <div id="right" class="border-l-2 border-l-text-damped p-2 pr-0 w-96">
            <h1 class="text-2xl font-bold text-center">Statistics:</h1>
            {#if audioFileMetadata !== null}
            <ul class="mt-4 text-lg gap-2 flex flex-col">
                <!-- <li class="px-3 py-1 border-2 bg-elevated border-elevated rounded-md">
                    <div>Original file name:</div>
                    <div class="text-right">{audioFileMetadata.originalFileName}</div>
                </li> -->
                <!-- {#each Object.entries(audioFileMetadata) as [key, value]}
                    <ListItem value={value} valuePosition="text-right">{key}</ListItem>
                {/each} -->
                <ListItem value={audioFileMetadata.originalFileName} valuePosition="text-right">Original filename:</ListItem>
                <ListItem value={audioFileMetadata.duration.toString()} valuePosition="text-right">Duration:</ListItem>
                <ListItem value={audioFileMetadata.codec} valuePosition="text-right">Codec:</ListItem>
                <ListItem value={audioFileMetadata.bitrate?.toString()} valuePosition="text-right">Bitrate:</ListItem>
                <ListItem value={audioFileMetadata.sampleRate.toString()} valuePosition="text-right">Sample Rate:</ListItem>
                <ListItem value={audioFileMetadata.channels.toString()} valuePosition="text-right">Channels:</ListItem>
                <ListItem value={audioFileMetadata.size.toString()} valuePosition="text-right">Size:</ListItem>





            </ul>
            {:else}
                <div class="mt-10 text-center bg-danger py-10 rounded-xl flex flex-col items-center gap-4">
                    <h1 class="text-lg px-2">File not uploaded, can't extract information</h1>
                    <Button name="Upload now!" href="/" color="alert"></Button>
                </div>
            {/if}
        </div>
    </div>
</div>