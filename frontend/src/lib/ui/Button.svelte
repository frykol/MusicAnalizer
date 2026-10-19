<script lang="ts">
    interface Props {
        name: string;
        href: string;
    }

    let { name, href }: Props = $props();
</script>

<a href="{href}" class="text-xl border-2 border-elevated px-2 py-1 rounded-md cursor-pointer hover:bg-elevated transition-colors">{name}</a>