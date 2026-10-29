<script lang="ts">

</script>

<div class="flex flex-row min-h-full">
    <div class="flex-1">
        <div class="flex flex-col items-center justify-center">
            <h1 class="text-3xl font-bold">Log in into your account</h1>
        </div>
        <form method="POST" action="?/login" class="mt-10">
            <div class="flex flex-col justify-center items-center gap-5">
                <label class="flex gap-10 justify-between">
                    <span class="text-xl">Username:</span>
                    <input name="username" type="text" class="border-b-2" placeholder="username">
                </label>
                <label class="flex gap-10 justify-between">
                    <span class="text-xl">Password:</span>
                    <input name="password" type="password" class="border-b-2" placeholder="password">
                </label>
            </div>
            <button>Login</button>
        </form>
    </div>
    <div class="w-1 bg-text-damped"></div>
    <div class="flex-1">
        <div class="flex flex-col items-center justify-center">
            <h1 class="text-3xl font-bold">Don't have account?</h1>
            <p class="text-2xl text-text-damped">Register now</p>
        </div>
    </div>
</div>