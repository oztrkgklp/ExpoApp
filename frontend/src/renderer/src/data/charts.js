
import { faPills, faTablets, faCapsules,faCannabis } from '@fortawesome/free-solid-svg-icons'; 
const trafficShares = [
    { id: 1, label: "Satış", value: 40, color: "secondary", icon: faPills },
    { id: 2, label: "Alış", value: 30, color: "primary", icon: faTablets },
    { id: 3, label: "Kar", value: 20, color: "tertiary", icon: faCapsules },
    {id: 4, label: "Zarar", value: 10, color: "danger", icon: faCannabis}
];

const totalOrders = [
    { id: 1, label: "July", value: [1, 5, 2, 5, 4, 3], color: "primary" },
    { id: 2, label: "August", value: [2, 3, 4, 8, 1, 2], color: "secondary" }
];

export {
    trafficShares,
    totalOrders
};