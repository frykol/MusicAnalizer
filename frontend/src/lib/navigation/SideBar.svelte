<script lang="ts">
    interface ListItem {
        name: string;
        href: string;
    }

    const tracks: ListItem[] = [
        { name: 'All tracks', href: '/tracks' },
        { name: 'Create track', href: '/tracks/create' }
    ];


</script>

<div class="bg-surface h-full">
    <div class="p-4" id="tracks">
        <h1 class="text-xl font-bold border-b-2 border-b-text-damped">Tracks</h1>
        <ul class="flex flex-col items-center">
            {#each tracks as track}
                <li class="w-full text-center"><a href={track.href} class="block w-full px-2 py-2 border-b-2 hover:rounded-md hover:rounded-t-none border-b-elevated hover:bg-elevated transition-colors">{track.name}</a></li>
            {/each}
        </ul>
    </div>
</div>