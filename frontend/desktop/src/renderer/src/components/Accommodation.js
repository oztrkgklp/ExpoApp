import React from 'react'
import ReactDataSheet from 'react-datasheet';
import 'react-datasheet/lib/react-datasheet.css';

const Accommodation = () => {

  
    const [grid, setGrid] = React.useState([
        [
            { value: " ", readOnly: true, },
            { value: "Şirket Adı ", readOnly: true, },
            { value: "Hotel", readOnly: true, },
            { value: "Check-in Tarihi", readOnly: true, },
            { value: "Misafir Adı", readOnly: true, },
            { value: "Misafir Adı", readOnly: true, },
            { value: "Misafir Adı", readOnly: true, },
            { value: "Firma Adı", readOnly: true, },
            { value: "Telefon", readOnly: true, },
            { value: "SNG", readOnly: true, },
            { value: "DBL", readOnly: true, },
            { value: "TRPL", readOnly: true, },
            { value: "QUAT", readOnly: true, },
            { value: "SNG+CHD", readOnly: true, },
            { value: "DBL+CHD", readOnly: true, },
            { value: "TRPL+CHD", readOnly: true, },
            { value: "Check-out Tarihi", readOnly: true, },
            { value: "SNG", readOnly: true, },
            { value: "DBL", readOnly: true, },
            { value: "TRPL", readOnly: true, },
            { value: "QUAT", readOnly: true, },
            { value: "SNG+CHD", readOnly: true, },
            { value: "DBL+CHD", readOnly: true, },
            { value: "TRPL+CHD", readOnly: true, },
            { value: "Açıklama", readOnly: true, },
        ],
        [
            { readOnly: true, value: 1 },
            { value: "" },
            { value: "" },
            { value: "" },
            { value: "" },
            { value: "" },
            { value: "" },
            { value: "" },
            { value: "" },
            { value: "" },
            { value: "" },
            { value: "" },
            { value: "" },
            { value: "" },
            { value: "" },
            { value: "" },
            { value: "" },
            { value: "", readOnly: true },
            { value: "", readOnly: true },
            { value: "", readOnly: true },
            { value: "", readOnly: true },
            { value: "", readOnly: true },
            { value: "", readOnly: true },
            { value: "", readOnly: true, },
            { value: "" },
        ]
    ]);
    const cost = { sng: 100, dbl: 200, trpl: 300, quat: 400, sngchd: 500, dblchd: 600, trplchd: 700 };


    return (
        <div>
            <ReactDataSheet
                data={grid}
                valueRenderer={cell => cell.value

                }
                onCellsChanged={changes => {
                    const grid_ = grid.map(row => [...row]);
                    changes.forEach(({ cell, row, col, value }) => {
                        
                        grid_[row][col] = { ...grid_[row][col], value }
                        if (grid_[row][9].value !== "") {
                            
                            grid_[row][17].value = `${parseInt(grid_[row][9].value) * cost.sng} `

                        }
                        if (grid_[row][10].value !== "") {

                            grid_[row][18].value = `${parseInt(grid_[row][10].value) * cost.dbl} `

                        }
                        if (grid_[row][11].value !== "") {

                            grid_[row][19].value = `${parseInt(grid_[row][11].value) * cost.trpl} `

                        }
                        if (grid_[row][12].value !== "") {

                            grid_[row][20].value = `${parseInt(grid_[row][12].value) * cost.quat} `

                        }
                        if (grid_[row][13].value !== "") {

                            grid_[row][21].value = `${parseInt(grid_[row][13].value) * cost.sngchd} `

                        }
                        if (grid_[row][14].value !== "") {

                            grid_[row][22].value = `${parseInt(grid_[row][14].value) * cost.dblchd} `

                        }
                        if (grid_[row][15].value !== "") {

                            grid_[row][23].value = `${parseInt(grid_[row][15].value) * cost.trplchd} `

                        }

                        if(row === grid_.length - 1)
                        {
                            if(grid_[row][col].value === "" || grid_[row][col].value === null ) {}
                            else
                            grid_.push([
                                { readOnly: true, value: grid.length },
                                { value: "" },
                                { value: "" },
                                { value: "" },
                                { value: "" },
                                { value: "" },
                                { value: "" },
                                { value: "" },
                                { value: "" },
                                { value: "" },
                                { value: "" },
                                { value: "" },
                                { value: "" },
                                { value: "" },
                                { value: "" },
                                { value: "" },
                                { value: "" },
                                { value: "", readOnly: true },
                                { value: "", readOnly: true },
                                { value: "", readOnly: true },
                                { value: "", readOnly: true },
                                { value: "", readOnly: true },
                                { value: "", readOnly: true },
                                { value: "", readOnly: true, },
                                { value: "" },
                            ])
                        }
                        

                        



                    })
                    setGrid(grid_);

                }}
            />
        </div>
    )
}


export default Accommodation
