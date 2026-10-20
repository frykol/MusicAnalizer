<script lang="ts">
	import type { Snippet } from "svelte";

    type TextPosition = "text-right" | "text-left" | "text-center"

    interface Props{
        children: Snippet;
        value: string;
        valuePosition?: TextPosition
    }

    let {children, value, valuePosition = "text-left"}: Props = $props()
</script>

<li class="px-3 py-1 border-2 bg-elevated border-elevated rounded-md">
    <div>{@render children()}</div>
    <div class="{valuePosition}">{value}</div>
</li>