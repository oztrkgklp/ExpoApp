import { faPills, faTablets, faCapsules,faCannabis } from '@fortawesome/free-solid-svg-icons'; 

const trafficShares = [
    { id: 1, label: "Katılıp Alım Yapanlar", value:60, color: "secondary", icon: faPills },
    { id: 2, label: "Katılıp Alım Yapmayanlar ", value:15, color: "primary", icon: faTablets },
    { id: 3, label: "Katılmayanlar", value:25, color: "tertiary", icon: faCapsules },
];

const totalOrders = [
    { id: 1, label: "July", value: [1, 5, 2, 5, 4, 3], color: "primary" },
    { id: 2, label: "August", value: [2, 3, 4, 8, 1, 2], color: "secondary" }
];

export {
    trafficShares,
    totalOrders
};