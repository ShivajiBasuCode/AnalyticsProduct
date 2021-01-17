import React from 'react';
import { makeStyles } from '@material-ui/core/styles';
import Paper from '@material-ui/core/Paper';
import Tabs from '@material-ui/core/Tabs';
import Tab from '@material-ui/core/Tab';
import PhoneIcon from '@material-ui/icons/Phone';
import FavoriteIcon from '@material-ui/icons/Favorite';
import PersonPinIcon from '@material-ui/icons/PersonPin';
import DescriptionTwoToneIcon from '@material-ui/icons/DescriptionTwoTone';
import Tooltip from '@material-ui/core/Tooltip';
import FileImport from './FileImport';
import Box from '@material-ui/core/Box';
import AliasName from './AliasName';
const useStyles = makeStyles({
    root: {
        flexGrow: 2,
        maxWidth: "100%",
    },
    div: {
        display: 'flex',
        flexWrap: 'wrap',
        '& > *': {
            margin: "1px",
            width: "100%",
            //height: "100px",
        },
    }
});



export default function ImportContainer() {
    // const classes = useStyles();
    const [value, setValue] = React.useState(0);

    const handleChange = (event, newValue) => {
        setValue(newValue);
    };

    return (
        <>

            <Paper square elevation={5} >
                <Tabs
                    value={value}
                    onChange={handleChange}
                    variant="fullWidth"
                    indicatorColor="primary"
                    textColor="primary"
                    aria-label="icon tabs example"
                >
                    <Tooltip title="Data from flat files (csv)" aria-label="Data from flat files (csv)">
                        <Tab icon={<DescriptionTwoToneIcon />} aria-label="Data from flat files (csv)" />
                    </Tooltip>
                    <Tab icon={<FavoriteIcon />}  ria-label="favorite" />
                    <Tab icon={<FavoriteIcon />} disabled aria-label="favorite" />
                    <Tab icon={<PersonPinIcon />} disabled aria-label="person" />
                </Tabs>

                {value == 0 &&

                    <div
                        role="tabpanel">
                        <Box p={3} height="100%">
                            <FileImport />
                        </Box>
                    </div>
                }
                 {value == 1 &&

                    <div
                        role="tabpanel">
                        <Box p={3} height="100%">
                            <AliasName />
                        </Box>
                    </div>
                    }
            </Paper>

        </>
    );
}
