// Copyright 2018 John Pursey
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapCombiner
{
    interface ICommand
    {
        void Redo();
        void Undo();
    }

    class Commands
    {
        public Commands()
        {
            m_index = 0;
            m_commands = new List<ICommand>();
        }

        public bool CanUndo { get { return m_index > 0; } }
        public bool CanRedo { get { return m_index < m_commands.Count; } }

        public void Do(ICommand command)
        {
            if (m_index < m_commands.Count)
                m_commands.RemoveRange(m_index, m_commands.Count - m_index);
            command.Redo();
            m_commands.Add(command);
            m_index = m_commands.Count;
        }

        public void Undo()
        {
            if (CanUndo)
            {
                m_index -= 1;
                m_commands[m_index].Undo();
            }
        }

        public void Redo()
        {
            if (CanRedo)
            {
                m_commands[m_index].Redo();
                m_index += 1;
            }
        }

        public void Reset()
        {
            m_commands.Clear();
            m_index = 0;
        }

        private int m_index;
        private List<ICommand> m_commands;
    }
}
