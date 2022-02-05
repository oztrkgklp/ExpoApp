import React from "react";
import { strToDate, dateFormat2 } from "../assets/dateTime";
import { accommodations } from "./FetchData";
import Lock from "./Lock";

const DailyAccommodations = () => {
  const [accommodation, setAccommodation] = React.useState([]);
  React.useEffect(() => {
    const accData = async () => {
      const acc = await accommodations();
      setAccommodation(acc.result);
    };

    accData();
  }, []);

  const accTable = accommodation.map((checkIn) => checkIn.checkIn).sort((a,b) => strToDate(dateFormat2(a)) - strToDate(dateFormat2(b)));
  const filitre = accTable.filter((item, index) => {
    return accTable.indexOf(item) === index;
  });

  return (
    <div>
      {filitre.map((item, index) => (
        <Lock props={item} />
      ))}
    </div>
  );
};

export default DailyAccommodations;
