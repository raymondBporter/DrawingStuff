using System.Collections.Generic;

namespace DrawingStuff
{
    interface IWorldBoundingRect
    {
        Rect WorldBoundingRect { get; }
    }


    class ContactGroups
    {
        List<List<Visual>> Groups;
        List<int> GroupCounts = new List<int>();

        int NumGroups = 0;


        public void BuildGroup(Visual visual, int groupNumber)
        {
            if (visual.ContactGroup == -1)
            {
                visual.ContactGroup = groupNumber;
                GroupCounts[groupNumber]++;

                foreach (Visual contact in visual.Contacts)
                {
                    BuildGroup(contact, groupNumber);
                }
            }
        }


        public void Draw(List<Visual> visuals, Batch batch)
        {
            foreach (var visual in visuals)
            {
                visual.ContactGroup = -1;
            }

            for (int i = 0; i < visuals.Count; i++)
            {
                if (visuals[i].ContactGroup == -1)
                {
                    GroupCounts.Add(0);
                    BuildGroup(visuals[i], NumGroups);
                    NumGroups++;
                }
            }

            Groups = new List<List<Visual>>(NumGroups);

            for (int i = 0; i < NumGroups; i++)
            {
                Groups.Add(new List<Visual>(GroupCounts[i]));
            }

            foreach (Visual visual in visuals)
            {
                Groups[visual.ContactGroup].Add(visual);
            }

            for (int i = 0; i < Groups.Count; i++)
            {
                Groups[i].Sort((x, y) => -Comparer<float>.Default.Compare(x.ZCoord, y.ZCoord));
            }

            Material currentMaterial = null;
            int[] currentIndex = new int[Groups.Count];

            while (true)
            {
                // Find first non empty group;
                int groupIndex = 0;

                for (; groupIndex < Groups.Count; groupIndex++)
                {
                    if ( currentIndex[groupIndex] < Groups[groupIndex].Count )
                    {
                        currentMaterial = Groups[groupIndex][currentIndex[groupIndex]].Material;
                        break;
                    }
                }
                // All groups are empty
                if ( groupIndex  == Groups.Count)
                {
                    return;
                }

                batch.Begin(currentMaterial);

                for (; groupIndex < NumGroups; groupIndex++)
                {
                    for (; currentIndex[groupIndex] < GroupCounts[groupIndex]; currentIndex[groupIndex]++)
                    {
                        if (!Groups[groupIndex][currentIndex[groupIndex]].Material.CanBatchWith(currentMaterial))
                        {
                            break;
                        }
                        else
                        {
                            Visual v = Groups[groupIndex][currentIndex[groupIndex]];

                            if ( v.Contacts.Count != 0 )
                                v.Color = new Color4(1, 0, 0, 0.5f);
                            else
                                v.Color = new Color4(0, 1, 0, 0.5f);
      

                            
                            /*
                            if ( groupIndex == 0 )
                                v.Color = new Color4(1, 0, 0, 0.5f);
                            if (groupIndex == 1)
                                v.Color = new Color4(0, 1, 0, 0.5f);
                            if (groupIndex == 2)
                                v.Color = new Color4(0, 0, 1, 0.5f);
                            if (groupIndex == 3)
                                v.Color = new Color4(1, 1, 0, 0.5f);
                            if (groupIndex == 5)
                                v.Color = new Color4(1, 0, 1, 0.5f);
                            if (groupIndex == 6)
                                v.Color = new Color4(0, 1, 1, 0.5f);
                                */
                            batch.AddGeom(v, v.Material, v.Transform, v.UseTransform);
                        };
                    }
                }

                batch.End();
            }
        }
    }
}



