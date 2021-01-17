
import React from 'react';
import Tab from '@material-ui/core/Tab';
import { Box, Tabs, Typography } from '@material-ui/core';
import AppBar from '@material-ui/core/AppBar';
import AccessGroupsComponent from './AccessGroupsComponent';
import UsersComponent from './UsersComponent';
import AccessOperationComponent from './AccessOperationComponent';



interface TabPanelProps {
    children?: React.ReactNode;
    index: any;
    value: any;
}

function TabPanel(props: TabPanelProps) {
    const { children, value, index, ...other } = props;

    return (
        <div
            role="tabpanel"
            hidden={value !== index}
            id={`simple-tabpanel-${index}`}
            aria-labelledby={`simple-tab-${index}`}
            {...other}
        >
            {value === index && (
                <Box p={3}>
                    <Typography>{children}</Typography>
                </Box>
            )}
        </div>
    );
}

function a11yProps(index: any) {
    return {
        id: `simple-tab-${index}`,
        'aria-controls': `simple-tabpanel-${index}`,
    };
}

export default function UserManagementContainer() {
    
    const [value, setValue] = React.useState(0);

    const handleChange = (event: React.ChangeEvent<{}>, newValue: number) => {
        setValue(newValue);
    };

    return (<>
        <AppBar position="static">
            <Tabs value={value} onChange={handleChange} aria-label="simple tabs example">
                <Tab label="Users" {...a11yProps(0)} />
                <Tab label="Access Groups" {...a11yProps(1)} />
                <Tab label="Access operations" {...a11yProps(2)} />
            </Tabs>
        </AppBar>
        <TabPanel value={value} index={0}>
            <UsersComponent />
        </TabPanel>
        <TabPanel value={value} index={1}>
            <AccessGroupsComponent />
        </TabPanel>
        <TabPanel value={value} index={2}>
            <AccessOperationComponent />
       </TabPanel>
    </>
    );
}
