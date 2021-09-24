import React from "react";
import { faCashRegister, faChartLine,faBed } from "@fortawesome/free-solid-svg-icons";
import { Col, Row } from "@themesberg/react-bootstrap";

import {
  CounterWidget,
  CircleChartWidget,
  ProgressTrackWidget,
  SalesValueWidget,
  SalesValueWidgetPhone,
} from "../../components/Widgets";
import { PageVisitsTable } from "../../components/Tables";
import { trafficShares } from "../../data/charts";
import axios from "axios";
import { domain } from "../../assets/domain";
import useSWR from "swr";

var result = [];

axios
  .get(domain + "companies/count")
  .then(function ({ data }) {
    // handle success
    result = data.result;
  })
  .catch(function (error) {
    // handle error
    console.log(error);
  })
  .then(function () {
    // always executed
  });

export default () => {
  const fetcher = (...args) => fetch(...args).then((res) => res.json());
  const { data, error } = useSWR(domain + "companies/endorsement", fetcher);
  if (error) return <div>failed to load</div>;
  if (!data) return <div>loading...</div>;
  return (
    <>
      <Row className="justify-content-md-center">
        <Col xs={12} className="mb-4 d-none d-sm-block">
          <SalesValueWidget
            title="Satışlar"
            value="10,567"
            percentage={10.57}
          />
        </Col>
        <Col xs={12} className="mb-4 d-sm-none">
          <SalesValueWidgetPhone
            title="SatışLar"
            value="10,567"
            percentage={10.57}
          />
        </Col>
        <Col xs={12} sm={6} xl={4} className="mb-4">
          <CounterWidget
            category={"Misafirler "}
            title={result}
            percentage={18.2}
            icon={faChartLine}
            iconColor="shape-secondary"
          />
        </Col>

        <Col xs={12} sm={6} xl={4} className="mb-4">
          <CounterWidget
            category="Ciro"
            title={data.result + "₺"}
            period="5 Ekim - 8 Ekim"
            percentage={28.4}
            icon={faCashRegister}
            iconColor="shape-tertiary"
          />
        </Col>
        <Col xs={12} sm={6} xl={4} className="mb-4">
          <CounterWidget
            category="Konaklayan"
            title={290}
            period="5 Ekim - 8 Ekim"
            percentage={28.4}
            icon={faBed}
            iconColor="shape-tertiary"
          />
        </Col>
        <Col xs={12} sm={6} xl={4} className="mb-4">
          <CounterWidget
            category="Katılan Firma"
            title={185}
            period="5 Ekim - 8 Ekim"
            percentage={28.4}
            icon={faCashRegister}
            iconColor="shape-tertiary"
          />
        </Col>
        <Col xs={12} sm={6} xl={4} className="mb-4">
          <CounterWidget
            category="Katılmayan Firma"
            title={98}
            period="5 Ekim - 8 Ekim"
            percentage={28.4}
            icon={faCashRegister}
            iconColor="shape-tertiary"
          />
        </Col>
        

        <Col xs={12} sm={6} xl={4} className="mb-4">
          <CircleChartWidget title="Ciro Dağılımı" data={trafficShares} />
        </Col>
      </Row>
      <Row>
        <Col xs={12} xl={12} className="mb-4">
          <Row>
            <Col xs={12} xl={8} className="mb-4">
              <Row>
                <Col xs={12} className="mb-4">
                  <PageVisitsTable />
                </Col>
                <Col xs={12} lg={6} className="mb-4"></Col>
              </Row>
            </Col>
            <Col xs={12} xl={4}>
              <Row>
                <Col xs={12} className="mb-4">
                  <ProgressTrackWidget />
                </Col>
              </Row>
            </Col>
          </Row>
        </Col>
      </Row>
    </>
  );
};
