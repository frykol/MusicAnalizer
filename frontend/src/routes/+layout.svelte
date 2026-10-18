<script lang="ts">
	import '../app.css';
	import favicon from '#lib/assets/favicon.svg';
	import type { LayoutProps } from './$types';
	import TopBar from '#lib/navigation/TopBar.svelte';
	import SideBar from '#lib/navigation/SideBar.svelte';

	let { children }: LayoutProps = $props();
</script>

<svelte:head>
	<link rel="icon" href={favicon} />
</svelte:head>

<div class="flex min-h-dvh flex-col">
	<header class="sticky top-0 z-50">
		<TopBar />
	</header>
	<div class="flex flex-1 flex-row">
		<div class="sticky top-16 h-[calc(100dvh-4rem)] shrink-0 w-56">
			<SideBar/>
		</div>
		<div class="flex-1 min-w-0 mt-6 mx-6 bg-surface p-4 rounded-lg">
			{@render children()}
		</div>
	</div>
</div>