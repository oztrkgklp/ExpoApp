import { faPills, faTablets, faCapsules,faCannabis } from '@fortawesome/free-solid-svg-icons'; 
import axios from 'axios';
import { domain } from '../assets/domain';
var result = [];
axios.get(domain + "companies")
.then(function ({data}) {
  // handle success
  result = data.result;
  console.log(result)
})
.catch(function (error) {
  // handle error
  console.log(error);
})
.then(function () {
  // always executed
});
var result2
axios.get(domain + "companies/endorsement")
.then(function ({data}) {
  // handle success
  result2 = data.result;

})
.catch(function (error) {
  // handle error
  console.log(error);
})
.then(function () {
  // always executed
});
var percantages = result.map(r=>{
    return (r.endorsement/result2*100)
})
percantages.sort(function(a, b){return b - a});

const trafficShares = [
    { id: 1, label: "Test ", value:40, color: "secondary", icon: faPills },
    { id: 2, label: "Test ", value:15, color: "primary", icon: faTablets },
    { id: 3, label: "Test ", value:25, color: "tertiary", icon: faCapsules },
    {id: 4, label: "Test ", value:20, color: "danger", icon: faCannabis}
];

const totalOrders = [
    { id: 1, label: "July", value: [1, 5, 2, 5, 4, 3], color: "primary" },
    { id: 2, label: "August", value: [2, 3, 4, 8, 1, 2], color: "secondary" }
];

export {
    trafficShares,
    totalOrders
};