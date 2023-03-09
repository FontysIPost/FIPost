import {createWebHashHistory, createRouter} from "vue-router";
import RegisterPackage from "@/views/RegisterPackage.vue";
import Home from "@/views/Home.vue";
import PakketOverzicht from "@/views/PakketOverzicht.vue";
import PackagePage from "@/views/PackagePage.vue";
import AddLocation from "@/views/AddLocation.vue";
import LocationOverview from "@/views/LocationOverview.vue";
import Login from "@/views/Login.vue";
import SignUp from "@/views/Sign-up.vue";
import CityOverview from "@/components/location/CityOverview.vue";
import BuildingOverview from "@/components/location/BuildingOverview.vue";

const routes = [
    {
        path: "/registratie",
        name: "RegisterPackage",
        component: RegisterPackage,
    },
    {
        path: "/login",
        name: "Login",
        component: Login,
    },
    {
        path: "/sign-up",
        name: "SignUp",
        component: SignUp,
    },
    {
        path: "/",
        name: "Home",
        component: Home
    },
    {
        path: "/overzicht",
        name: "PakketOverzicht",
        component: PakketOverzicht
    },
    {
        path: "/pakket/:id",
        name: "PackagePage",
        component: PackagePage
    },
    {
        path: "/locaties/nieuw",
        name: "AddLocation",
        component: AddLocation
    },
    {
        path: "/locaties",
        name: "Locatie Overzicht",
        component: LocationOverview
    },
    {
        path: "/locaties/steden",
        name: "Steden Overzicht",
        component: CityOverview
    },
    {
        path: "/locaties/gebouwen",
        name: "Gebouwen Overzicht",
        component: BuildingOverview
    },
    {
        path: "/locaties/gebouwen",
        name: "Gebouwen Overzicht",
        component: BuildingOverview
    }
];

const router = createRouter({
    history: createWebHashHistory(),
    routes,
});

export default router;
