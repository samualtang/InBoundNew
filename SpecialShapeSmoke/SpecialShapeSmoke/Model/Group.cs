        using System;
        using System.Collections.Generic;
        using System.Linq;
        using System.Text;
        using OpcRcw.Da;
        using OpcRcw.Comn;
        using System.Runtime.InteropServices;

namespace SpecialShapeSmoke.Model
{
    public class Group : IOPCDataCallback
        {
        IOPCServer pIOPCServer;  //定义opcServer对象
        IOPCAsyncIO2 pIOPCAsyncIO2 = null;                   //读对象       // instance pointer for asynchronous IO.
        IOPCSyncIO pIOPCSyncIO = null;
        IOPCGroupStateMgt pIOPCGroupStateMgt = null;        //管理opcgroup对象
        IConnectionPointContainer pIConnectionPointContainer = null;
        IConnectionPoint pIConnectionPoint = null;
         public delegate void HandleDelegate(int group , int[] clientid,object[] values);

       public  HandleDelegate callback;
        Int32 dwRequestedUpdateRate = 250;
        Int32 pRevUpdateRate;
        float deadband = 0;
        Guid iidRequiredInterface = typeof(IOPCItemMgt).GUID;
        int TimeBias = 0;
        int pSvrGroupHandle = 0;  
        GCHandle hTimeBias, hDeadband;
        Object pobjGroup1 = null;      //groub对象
        Int32 dwCookie = 0;
        OPCITEMDEF[] ItemDeffArray;
        int[] ItemSvrHandleArray;
        public Group(IOPCServer server, int cliengGroup, string groupName, int isActive, int LOCALE_ID)
        {
            pIOPCServer = server;
        hTimeBias = GCHandle.Alloc(TimeBias, GCHandleType.Pinned);
        hDeadband = GCHandle.Alloc(deadband, GCHandleType.Pinned);
        pIOPCServer.AddGroup(groupName,
                isActive,
                dwRequestedUpdateRate,
                cliengGroup,
                hTimeBias.AddrOfPinnedObject(),
                hDeadband.AddrOfPinnedObject(),
                LOCALE_ID,
                out pSvrGroupHandle,
                out pRevUpdateRate,
                ref iidRequiredInterface,
                out pobjGroup1);
        InitReqIOInterfaces();
        }
        public void addItem(List<String> itemNameList)
        {
            if (itemNameList != null)
            {
                ItemDeffArray = new OPCITEMDEF[itemNameList.Count];
                for (int i = 0; i < itemNameList.Count; i++)
                {
                    ItemDeffArray[i].szAccessPath = "";                   // Accesspath not needed for this sample
                    ItemDeffArray[i].szItemID = itemNameList[i];          // Item ID,
                    ItemDeffArray[i].bActive = 1;                    // item is active
                    ItemDeffArray[i].hClient = i + 1;                    // client handle
                    ItemDeffArray[i].dwBlobSize = 0;                    // blob size
                    ItemDeffArray[i].pBlob = IntPtr.Zero;          // pointer to blob
                    ItemDeffArray[i].vtRequestedDataType = 2;
                }
                IntPtr pResults = IntPtr.Zero;
                IntPtr pErrors = IntPtr.Zero;

               
                    // Add items to group
                    ((IOPCItemMgt)pobjGroup1).AddItems(ItemDeffArray.Length, ItemDeffArray, out pResults, out pErrors);

                    // Unmarshal to get the server handles out fom the m_pItemResult
                    // after checking the errors
                    int[] errors = new int[ItemDeffArray.Length];
                    IntPtr pos = pResults;

                    ItemSvrHandleArray = new int[ItemDeffArray.Length];
                    Marshal.Copy(pErrors, errors, 0, ItemDeffArray.Length);
                    OPCITEMRESULT result;


                    for (int i = 0; i < ItemDeffArray.Length; i++)
                    {
                        if (errors[i] == 0)
                        {
                            if (i != 0)
                            {
                                pos = new IntPtr(pos.ToInt32() + Marshal.SizeOf(typeof(OPCITEMRESULT)));

                            }
                            result = (OPCITEMRESULT)Marshal.PtrToStructure(pos, typeof(OPCITEMRESULT));
                            ItemSvrHandleArray[i] = result.hServer;
                            //Marshal.DestroyStructure(pos, typeof(OPCITEMRESULT));
                        }
                        else
                        {


                        }
                    }
                }
            
        }
        private void InitReqIOInterfaces()
        {
            try
            {
                //Query interface for async calls on group object
                pIOPCAsyncIO2 = (IOPCAsyncIO2)pobjGroup1;
                pIOPCSyncIO = (IOPCSyncIO)pobjGroup1;
                pIOPCGroupStateMgt = (IOPCGroupStateMgt)pobjGroup1;

                // Query interface for callbacks on group object
                pIConnectionPointContainer = (IConnectionPointContainer)pobjGroup1;

                // Establish Callback for all async operations
                Guid iid = typeof(IOPCDataCallback).GUID;
                pIConnectionPointContainer.FindConnectionPoint(ref iid, out pIConnectionPoint);

                // Creates a connection between the OPC servers's connection point and
                // this client's sink (the callback object).
                pIConnectionPoint.Advise(this, out dwCookie);
            }
            catch (System.Exception error) // catch for group adding
            {
               
            }
        }

        public void Release()
        {
            try
            {
                if (dwCookie != 0)
                {
                    //pIConnectionPoint.Unadvise(dwCookie);
                    dwCookie = 0;
                }
                // Free the unmanaged COM memory
                //Marshal.ReleaseComObject(pIConnectionPoint);
                pIConnectionPoint = null;

                //Marshal.ReleaseComObject(pIConnectionPointContainer);
                pIConnectionPointContainer = null;

                if (pIOPCAsyncIO2 != null)
                {
                    //Marshal.ReleaseComObject(pIOPCAsyncIO2);
                    pIOPCAsyncIO2 = null;
                }
                if (pIOPCGroupStateMgt != null)
                {
                    //Marshal.ReleaseComObject(pIOPCGroupStateMgt);
                    pIOPCGroupStateMgt = null;
                }
                if (pobjGroup1 != null)
                {
                    //Marshal.ReleaseComObject(pobjGroup1);
                    pobjGroup1 = null;
                }
            }
            catch
            { }
        }
        public void SyncWrite(object values, int index)
        {
            int nCancelid;
            IntPtr pErrors = IntPtr.Zero;
            if (pIOPCAsyncIO2 != null)
            {
                try
                {   // Async write
                    pIOPCAsyncIO2.Write(1, new int[]{ItemSvrHandleArray[index]}, new object[]{values}, 1, out nCancelid, out pErrors);
                    int[] errors = new int[3];
                    Marshal.Copy(pErrors, errors, 0, 3);
                    if (errors[0] != 0)
                    {
                        System.Exception ex = new Exception("Error in reading item");
                        throw ex;
                    }
                }
                catch (System.Exception error)
                {

                }
                finally
                {
                    // Free the unmanaged COM memory
                    if (pErrors != IntPtr.Zero)
                    {
                        Marshal.FreeCoTaskMem(pErrors);
                        pErrors = IntPtr.Zero;
                    }
                }
            }

        }
        public void SyncWrite(object[] values)
        {
            int nCancelid;
            IntPtr pErrors = IntPtr.Zero;
            if (pIOPCAsyncIO2 != null)
            {
                try
                {   // Async write
                    pIOPCAsyncIO2.Write(ItemSvrHandleArray.Length, ItemSvrHandleArray, values, ItemSvrHandleArray.Length, out nCancelid, out pErrors);
                    int[] errors = new int[3];
                    Marshal.Copy(pErrors, errors, 0, 3);
                    if (errors[0] != 0)
                    {
                        System.Exception ex = new Exception("Error in reading item");
                        throw ex;
                    }
                }
                catch (System.Exception error)
                {
                  
                }
                finally
                {
                    // Free the unmanaged COM memory
                    if (pErrors != IntPtr.Zero)
                    {
                        Marshal.FreeCoTaskMem(pErrors);
                        pErrors = IntPtr.Zero;
                    }
                }
            }

        }
        public void SyncRead(int index)
        {
            int nCancelid;
            IntPtr pErrors = IntPtr.Zero;

            if (pIOPCAsyncIO2 != null)
            {
                try
                {   // Async read

                    pIOPCAsyncIO2.Read(1, new int[]{ItemSvrHandleArray[index]}, 1, out nCancelid, out pErrors);
                    int[] errors = new int[1];
                    Marshal.Copy(pErrors, errors, 0, 1);
                    if (errors[0] != 0)
                    {
                        String pstrError;

                    }
                }
                catch (System.Exception error)
                {

                }
                finally
                {
                    // Free the unmanaged COM memory
                    if (pErrors != IntPtr.Zero)
                    {
                        Marshal.FreeCoTaskMem(pErrors);
                        pErrors = IntPtr.Zero;
                    }
                }
            }
        }
        public void Write(object value, int index)
        {
            // Access unmanaged COM memory
            IntPtr pErrors = IntPtr.Zero;

            object[] values = new object[1];
            values[0] = value;

            try
            {
                pIOPCSyncIO.Write(1, new int[] { ItemSvrHandleArray[index] }, values, out pErrors);
                int[] errors = new int[1];
                Marshal.Copy(pErrors, errors, 0, 1);

               
            }
            catch (System.Exception error)
            {
              
            }
            finally
            {
                // Free the unmanaged COM memory
                if (pErrors != IntPtr.Zero)
                {
                    Marshal.FreeCoTaskMem(pErrors);
                    pErrors = IntPtr.Zero;
                }
            }
        }
        public object ReadD(int index)
        {
            // Access unmanaged COM memory
            IntPtr pItemValues = IntPtr.Zero;
            IntPtr pErrors = IntPtr.Zero;
            try
            {
                // Sync read from device
                pIOPCSyncIO.Read(OPCDATASOURCE.OPC_DS_DEVICE, 8, new Int32[] { ItemSvrHandleArray[index] }, out pItemValues, out pErrors);
                // Unmarshal the returned memory to get the item state out fom the ppItemValues
                // after checking errors
                int[] errors = new int[1];
                Marshal.Copy(pErrors, errors, 0, 1);
                //if (errors[0] == 0)
                //{
                OPCITEMSTATE pItemState = (OPCITEMSTATE)Marshal.PtrToStructure(pItemValues, typeof(OPCITEMSTATE));


                // Free indirect variant element, other indirect elements are freed by Marshal.DestroyStructure(...)
                //DUMMY_VARIANT.VariantClear((IntPtr)((int)pItemValues + 0));
                return pItemState.vDataValue;
                //}
                //else
                //{
                //    return -1;
                //}

                // Free indirect structure elements
                Marshal.DestroyStructure(pItemValues, typeof(OPCITEMSTATE));
            }
            catch (System.Exception error)
            {
                return -1;
            }
            finally
            {
                // Free the unmanaged COM memory
                if (pItemValues != IntPtr.Zero)
                {
                    Marshal.FreeCoTaskMem(pItemValues);
                    pItemValues = IntPtr.Zero;
                }
                if (pErrors != IntPtr.Zero)
                {
                    Marshal.FreeCoTaskMem(pErrors);
                    pErrors = IntPtr.Zero;
                }
            }
            return "";
        }
        public object Read(int index)
        {
            // Access unmanaged COM memory
            IntPtr pItemValues = IntPtr.Zero;
            IntPtr pErrors = IntPtr.Zero;
            try
            {
                // Sync read from device
                pIOPCSyncIO.Read(OPCDATASOURCE.OPC_DS_DEVICE, 1, new int[] { ItemSvrHandleArray[index] }, out pItemValues, out pErrors);
                // Unmarshal the returned memory to get the item state out fom the ppItemValues
                // after checking errors
                int[] errors = new int[1];
                Marshal.Copy(pErrors, errors, 0, 1);
                if (errors[0] == 0)
                {
                    OPCITEMSTATE pItemState = (OPCITEMSTATE)Marshal.PtrToStructure(pItemValues, typeof(OPCITEMSTATE));
                 

                    // Free indirect variant element, other indirect elements are freed by Marshal.DestroyStructure(...)
                    //DUMMY_VARIANT.VariantClear((IntPtr)((int)pItemValues + 0));
                    return pItemState.vDataValue;
                }
                else
                {
                    return -1;
                }

                // Free indirect structure elements
                Marshal.DestroyStructure(pItemValues, typeof(OPCITEMSTATE));
            }
            catch (System.Exception error)
            {
                return -1;
            }
            finally
            {
                // Free the unmanaged COM memory
                if (pItemValues != IntPtr.Zero)
                {
                    Marshal.FreeCoTaskMem(pItemValues);
                    pItemValues = IntPtr.Zero;
                }
                if (pErrors != IntPtr.Zero)
                {
                    Marshal.FreeCoTaskMem(pErrors);
                    pErrors = IntPtr.Zero;
                }
            }
            return "";
        }
        public void SyncRead()
        {
            int nCancelid;
            IntPtr pErrors = IntPtr.Zero;

            if (pIOPCAsyncIO2 != null)
            {
                try
                {   // Async read

                    pIOPCAsyncIO2.Read(ItemSvrHandleArray.Length, ItemSvrHandleArray, ItemSvrHandleArray.Length, out nCancelid, out pErrors);
                    int[] errors = new int[1];
                    Marshal.Copy(pErrors, errors, 0, 1);
                    if (errors[0] != 0)
                    {
                        String pstrError;
                     
                    }
                }
                catch (System.Exception error)
                {
                   
                }
                finally
                {
                    // Free the unmanaged COM memory
                    if (pErrors != IntPtr.Zero)
                    {
                        Marshal.FreeCoTaskMem(pErrors);
                        pErrors = IntPtr.Zero;
                    }
                }
            }
        }
        public void OnCancelComplete(int dwTransid, int hGroup)
        {
            int i = 0;
        }

        public void OnDataChange(int dwTransid, int hGroup, int hrMasterquality, int hrMastererror, int dwCount, int[] phClientItems, object[] pvValues, short[] pwQualities, OpcRcw.Da.FILETIME[] pftTimeStamps, int[] pErrors)
        {
            if (callback != null)
            {
                callback(hGroup, phClientItems,pvValues);
            }
            
        }

        public void OnReadComplete(int dwTransid, int hGroup, int hrMasterquality, int hrMastererror, int dwCount, int[] phClientItems, object[] pvValues, short[] pwQualities, OpcRcw.Da.FILETIME[] pftTimeStamps, int[] pErrors)
        {
            int i = 0;
        }

        public void OnWriteComplete(int dwTransid, int hGroup, int hrMastererr, int dwCount, int[] pClienthandles, int[] pErrors)
        {
            int i = 0;
        }

        [ComVisible(true), StructLayout(LayoutKind.Sequential, Pack = 2)]
        public class DUMMY_VARIANT
        {
            [DllImport("oleaut32.dll")]
            public static extern int VariantClear(IntPtr addrofvariant);
        }
        }
}
