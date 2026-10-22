<script lang="ts">
    import type {UiColors} from "#lib/types/UiTypes.ts"

    interface Props {
        name: string;
        href: string;
        color?: UiColors
    }

    let { name, href, color = 'default' }: Props = $props();

    const variants = {
        default: 'border-elevated hover:bg-elevated',
        danger: 'border-danger hover:bg-danger',
        alert : 'border-alert hover:bg-alert',
        info: 'border-info hover:bg-info',
        success: 'border-success hover:bg-success'
    } satisfies Record<UiColors, string>;

</script>

<a href="{href}" class="text-xl border-2 px-2 py-1 rounded-md cursor-pointer transition-colors {variants[color]}">{name}</a>