import { Col, Row } from "@themesberg/react-bootstrap";
import React from "react";
import {
  ProformaTable,
  ExternalTable,
  ExternalTable2,
  ExternalTable3,
  ExternalTable4,
} from "./ProformaTable";

const Proforma = () => {
  return (
    <div>
      <ProformaTable />
      <Row style={{ marginTop: "2%" }}>
        <Col>
          <ExternalTable />
        </Col>
        <Col>
          <ExternalTable2 />
        </Col>
        <Col>
          <ExternalTable3 />
        </Col>
      </Row>
      <Row style={{marginTop:"2%"}}>
        <Col></Col>
        <Col>
          <ExternalTable4 />
        </Col>
        <Col></Col>
      </Row>
    </div>
  );
};

export default Proforma;
