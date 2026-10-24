<script lang="ts">
	import type { TrackSimple } from "#lib/api/tracks.ts";

    interface Props{
        track: TrackSimple
    };

    let { track }: Props = $props();
</script>
<a href="/tracks/{track.id}">
<div class="flex w-64 flex-col rounded-sm bg-elevated">

    <div class="flex min-w-0 justify-between gap-2 px-2 py-1">
        <h1 class="min-w-0 flex-1 wrap-break-word text-xl">
            <b>{track.title}</b>
        </h1>

        <p class="shrink-0 text-text-damped">
            id: {track.id}
        </p>
    </div>

    <p class="px-2 pb-1 text-sm">By: <b>Artist</b></p>

    <img
        src="/images/music_placeholder.webp"
        alt=""
        class="w-full aspect-square object-cover"
    />

    <p class="min-w-0 wrap-break-word p-2 text-text-damped text-sm">
        Genre: <b>{track.genre}</b>
    </p>
</div>
</a>
