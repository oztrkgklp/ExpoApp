import React from "react";
import { ProformaTable, ExternalTable } from "./ProformaTable";
const Proforma = () => {
  return (
    <div>
      <ProformaTable /> 
     
      <div style={{marginTop:"30px"}}>
        <ExternalTable />
      </div>
    </div>
  );
};

export default Proforma;
