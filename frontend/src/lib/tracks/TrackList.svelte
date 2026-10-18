<script lang="ts">
	import type { TrackSimple } from "#lib/api/tracks.ts";
	import TrackCard from "./TrackCard.svelte";

    interface Props{
        tracks: TrackSimple[]
    };
    let {tracks}: Props = $props();

</script>

<div class=' flex flex-row flex-1'>
    {#each tracks as track}
        <TrackCard track={track}/>
    {/each}
</div>


