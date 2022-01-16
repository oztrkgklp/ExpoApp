import React from "react";
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

  const accTable = accommodation.map((checkIn) => checkIn.checkIn);
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
