<script lang="ts">
	import type { TrackSimple } from "#lib/api/tracks.ts";
    import TrackCard from "#lib/tracks/TrackCard.svelte";
	import TrackList from "#lib/tracks/TrackList.svelte";
    import type { PageProps } from "./$types";

    let { params, data }: PageProps = $props();
    const tracks : TrackSimple[] = $derived(data.tracks);
</script>

<div class="flex justify-center">
<TrackList tracks={tracks}/>
</div> 