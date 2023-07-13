<script>
	export let items;
	import Menu from './Menu.svelte';
</script>

<ul>
	{#each items as item}
		<li>{item.Name}</li>
		{#if item.Children}
			<Menu items={item.Children} />
		{/if}
	{/each}
</ul>
