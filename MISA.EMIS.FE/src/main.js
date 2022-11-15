import { createApp } from 'vue'
import {createRouter, createWebHistory} from 'vue-router'
import App from './App.vue'
import  StaffList  from "./components/page/StaffList.vue"
import OverView from "./components/page/OverView.vue"
import BaseButton from './components/base/BaseButton.vue'
import BaseCheckbox from './components/base/BaseCheckbox.vue'
import BaseLoader from './components/base/BaseLoader.vue'
import 'devextreme/dist/css/dx.light.css'

const routers = [
    {path: "/overview", component: OverView},
    {path: "/device", component: OverView},
    {path: "/borrow", component: OverView},
    {path: "/report", component: OverView},
    {path: "/system", component: StaffList},
]

const router = createRouter({
    // 4. Provide the history implementation to use. We are using the hash history for simplicity here.
    history: createWebHistory(),
    routes: routers, // short for `routes: routes`
})

const app = createApp(App);
app.component("BaseButton", BaseButton);
app.component("BaseCheckbox", BaseCheckbox);
app.component("BaseLoader", BaseLoader);

app.use(router).mount('#app')
