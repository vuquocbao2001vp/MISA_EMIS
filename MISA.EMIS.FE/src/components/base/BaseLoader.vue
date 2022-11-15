<template>
    <div class="lds-ellipsis font-body" v-bind:class="{ hideLoader: isHideLoader }">
        <div></div>
        <div></div>
        <div></div>
        <div></div>
        <div></div>
        <div class="lds-text">{{Resource.LOADER.LoadingText}}</div>
    </div>
</template>

<script>
import MISAResource from '../../script/constants/MISAResource.js'
export default {
    props: ["isHideLoader"],
    created() {
        this.Resource = MISAResource;
    }
}
</script>

<style scoped>
.hideLoader {
    display: none;
}

.lds-ellipsis {
    position: absolute;
    top: calc((100% - 60px) / 2);
    left: calc((100% - 108px) / 2);;
    transform: translate(-50%, -50%);
    background-color: rgb(216, 216, 216);
    border-radius: 4px;
    width: 108px;
    height: 60px;
    z-index: 10;
}

.lds-ellipsis div {
    position: absolute;
    top: 12px;
    width: 12px;
    height: 12px;
    border-radius: 50%;
    animation-timing-function: cubic-bezier(0, 1, 1, 0);
}

.lds-ellipsis div:nth-child(1) {
    left: 24px;
    animation: lds-ellipsis1 0.6s infinite;
    background: #FA3939;
}

.lds-ellipsis div:nth-child(2) {
    left: 24px;
    animation: lds-ellipsis2 0.6s infinite;
    background: rgb(210, 120, 17);
}

.lds-ellipsis div:nth-child(3) {
    left: 44px;
    animation: lds-ellipsis2 0.6s infinite;
    background: rgb(253, 175, 80);
}

.lds-ellipsis div:nth-child(4) {
    left: 68px;
    animation: lds-ellipsis3 0.6s infinite;
    background: rgb(106, 151, 38);
}

.lds-ellipsis div.lds-text {
    width: 100%;
    position: absolute;
    top: 30px;
    left: 26px;
}

@keyframes lds-ellipsis1 {
    0% {
        transform: scale(0);
    }

    100% {
        transform: scale(1);
    }
}

@keyframes lds-ellipsis3 {
    0% {
        transform: scale(1);
    }

    100% {
        transform: scale(0);
    }
}

@keyframes lds-ellipsis2 {
    0% {
        transform: translate(0, 0);
    }

    100% {
        transform: translate(24px, 0);
    }
}
</style>
